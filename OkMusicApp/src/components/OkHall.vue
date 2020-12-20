<template>
  <el-card>
    <audio
      controls
      v-if="current != null"
      :src="currentUrl"
      autoplay
      @ended="onCurrentEnded"
    ></audio>
    <span>{{ current != null ? current.title : "" }}</span>
    <button :disabled="!iAmMaster" @click="previous()">previous</button>
    <button :disabled="!iAmMaster" @click="next()">next</button>
    <el-switch v-model="iAmMaster"></el-switch>
  </el-card>
  <el-card>
    <template #header>
      <h1>OkHall</h1>
    </template>
    <el-table :data="playlist" :show-header="false">
      <el-table-column type="index" width="50"> </el-table-column>
      <el-table-column width="50">
        <template #default="scope">
          <span>{{
            current != null && scope.row.playId == current.playId ? ">" : ""
          }}</span>
        </template>
      </el-table-column>
      <el-table-column prop="title"> </el-table-column>
      <el-table-column prop="duration" :formatter="durationFormatter">
      </el-table-column>
      <el-table-column prop="creator"> </el-table-column>
      <!-- <el-table-column>
        <template #default="scope">
          <span @click="star(scope.row)">💗❤️🧡</span>
        </template>
      </el-table-column> -->
    </el-table>
  </el-card>
  <el-card>
    <template #header>
      <h1>Chat</h1>
    </template>
    <div>
      <ul style="text-align: left">
        <li v-for="item in messages" :key="item" :class="item.type">
          {{ item.message }}
        </li>
      </ul>

      <el-input
        v-model="message"
        autofocus="true"
        ref="messageBox"
        @keyup.enter.native="sendMessage"
      >
        <template #append>
          <el-button @click="sendMessage">发送</el-button>
        </template>
      </el-input>
    </div>
  </el-card>
</template>

<script>
import * as signalR from "@microsoft/signalr";

export default {
  name: "OkHall",
  props: {
    msg: String,
  },
  data() {
    return {
      users: [],
      playlist: [],
      current: null,
      master: null,
      messages: [],
      message: "",
    };
  },
  created() {
    var connection = new signalR.HubConnectionBuilder()
      .withUrl("/ws/okhall")
      .build();

    connection.on("Next", this.onNext);
    connection.on("NewUserJoin", this.onNewUserJoin);
    connection.on("UserLeave", this.onUserLeave);
    connection.on("OkHall", this.onOkHall);
    connection.on("SendMessage", this.onSendMessage);
    connection.on("Push", this.onPush);
    connection.on("SetMaster", this.onSetMaster);

    connection
      .start()
      .then(this.onStart)
      .catch(function (err) {
        return console.error(err.toString());
      });

    window.vue = this;
    window.conn = connection;
  },
  watch: {
    messages: {
      handler(newValue) {
        if (newValue.filter((x) => x.type == "success").length > 10) {
          newValue.shift();
        }
      },
      deep: true,
    },
  },
  computed: {
    currentUrl() {
      return this.current != null ? "/static/" + this.current.fileName : "";
    },
    userId() {
      if (document.cookie == null || document.cookie.indexOf("userId") == -1) {
        return null;
      } else {
        return document.cookie.substring(7);
      }
    },
    iAmMaster: {
      get: function () {
        return this.master == this.userId;
      },
      set: function (switchMaster) {
        if (switchMaster == true) {
          window.conn.invoke("SetMaster", this.userId);
        } else {
          window.conn.invoke("SetMaster", null);
        }
      },
    },
  },
  methods: {
    durationFormatter(row, column, cellValue, index) {
      var date = new Date(cellValue);
      return date.getMinutes() + ":" + date.getSeconds();
    },
    onStart() {
      this.systemMessage("Start");
    },
    onOkHall(okHall) {
      this.systemMessage("OkHall:" + JSON.stringify(okHall));
      this.playlist = okHall.jukeBox.playlist;
      this.users = okHall.users;
      this.master = okHall.master;

      if (
        this.playlist != null &&
        this.playlist.length != 0 &&
        okHall.jukeBox.current != null
      ) {
        this.current = this.playlist.find(
          (x) => x.playId == okHall.jukeBox.current.playId
        );
        this.current.playing = true;
      }
    },
    onNext(playId) {
      this.current = this.playlist.find((x) => x.playId == playId);
      this.systemMessage("Next:" + playId);
    },
    onNewUserJoin() {
      this.systemMessage("NewUserJoin:");
    },
    onUserLeave() {
      this.systemMessage("UserLeave:");
    },
    onSendMessage(message) {
      this.messages.push({ message: message, type: "success" });
      console.log("SendMessage:" + message);
    },
    onPush(jukeBoxMusic) {
      this.playlist.push(jukeBoxMusic);
      this.systemMessage("Push:" + JSON.stringify(jukeBoxMusic));
    },
    onSetMaster(master) {
      this.master = master;
      this.systemMessage("SetMaster:" + master);
    },
    onCurrentEnded(event) {
      console.log("event" + event);
      if (this.iAmMaster) {
        console.log("iAmMaster" + this.iAmMaster);
        this.next();
      }
    },
    next() {
      var index = -1;

      //
      if (this.current != null) {
        index = this.playlist.findIndex((x) => x.playId == this.current.playId);
      }

      if (index < this.playlist.length - 1) {
        index++;
        this.current = this.playlist[index];

        window.conn.invoke("Next", this.current.playId);
      }
    },
    previous() {
      var index = this.playlist.findIndex(
        (x) => x.playId == this.current.playId
      );
      if (index != 0) {
        index--;
        this.current = this.playlist[index];

        window.conn.invoke("Next", this.current.playId);
      }
    },
    sendMessage() {
      if (this.message == "") return;
      window.conn.invoke("SendMessage", this.message);
      this.message = "";
      this.$refs["messageBox"].focus();
    },
    systemMessage(message) {
      this.messages.push({ message: message, type: "info" });
    },
  },
};
</script>

<style scoped>
.info {
  font-size: 12px;
  color: grey;
}

.success {
  font-size: 16px;
  color: black;
}
</style>