<template>
  <el-card>
    <template #header>
      <h1>Chat</h1>
    </template>
    <div>
      <ol>
        <li v-for="item in messages" :key="item">{{ item }}</li>
      </ol>
      <el-input v-model="message" />
      <button @click="sendMessage">发送</button>
    </div>
  </el-card>
</template>

<script>
export default {
  name: "Chat",
  props: {},
  data() {
    return {
      messages: [],
      message: "",
    };
  },
  created() {
    window.conn.on("SendMessage", function (message) {
      this.messages.push(message);
    });
    window.conn.start();
  },
  methods: {
    sendMessage() {
      this.messages.push(this.message);
      window.conn.invoke("SendMessage", this.message);
    },
  },
};
</script>