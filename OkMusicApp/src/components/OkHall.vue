<template>
  <el-card>
    <template #header>
      <h1>OkHall</h1>
    </template>
    <audio v-if="current != null" :src="currentUrl" autoplay></audio>
    <el-table :data="playlist" :show-header="false">
      <el-table-column type="index" width="50"> </el-table-column>
      <el-table-column width="50">
        <template #default="scope">
          <span>{{ scope.row.playId == current.playId ? ">" : "" }}</span>
        </template>
      </el-table-column>
      <el-table-column prop="title"> </el-table-column>
      <el-table-column prop="duration" :formatter="durationFormatter">
      </el-table-column>
      <el-table-column prop="creator"> </el-table-column>
    </el-table>
    <span>{{ current != null ? current.title : "" }}</span>
    <button @click="next()">next</button>
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

    connection
      .start()
      .then(this.onStart)
      .catch(function (err) {
        return console.error(err.toString());
      });

    window.vue = this;
    window.conn = connection;
  },
  computed: {
    currentUrl() {
      return this.current != null ? "/static/" + this.current.fileName : "";
    },
  },
  methods: {
    durationFormatter(row, column, cellValue, index) {
      var date = new Date(cellValue);
      return date.getMinutes() + ":" + date.getSeconds();
    },
    onStart() {
      console.log("Start");
    },
    onOkHall(okHall) {
      console.log("OkHall:" + JSON.stringify(okHall));
      this.playlist = okHall.jukeBox.playlist;
      this.users = okHall.users;

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
      console.log("Next:" + playId);
    },
    onNewUserJoin() {
      console.log("NewUserJoin:");
    },
    onUserLeave() {
      console.log("UserLeave:");
    },
    onSendMessage(message) {
      console.log("SendMessage:" + message);
    },
    onPush(jukeBoxMusic) {
      this.playlist.push(jukeBoxMusic);
      console.log("Push:" + JSON.stringify(jukeBoxMusic));
    },
    next() {
      var index = this.playlist.findIndex(
        (x) => x.playId == this.current.playId
      );
      if (index < this.playlist.length - 1) {
        index++;
        this.current = this.playlist[index];

        window.conn.invoke("Next", this.current.playId);
      }
    },
  },
};
</script>
