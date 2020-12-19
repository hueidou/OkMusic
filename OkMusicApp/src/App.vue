<template>
  <!-- <OkHall msg="Hello Vue 3.0 + Vite" /> -->

  <FavouriteList></FavouriteList>
</template>

<script>
import OkHall from "./components/OkHall.vue";
import FavouriteList from "./components/FavouriteList.vue";
import * as signalR from "@microsoft/signalr";

export default {
  name: "App",
  components: {
    OkHall,
    FavouriteList,
  },
  created() {

    var connection = new signalR.HubConnectionBuilder()
      .withUrl("/ws/okhall")
      .build();

    connection.on("Next", function (playId) {
      console.log("Next:" + playId);
    });

    connection.on("NewUserJoin", function () {
      console.log("NewUserJoin:");
    });

    connection.on("UserLeave", function () {
      console.log("UserLeave:");
    });

    connection.on("OkHall", function (okHall) {
      console.log("OkHall:" + JSON.stringify(okHall));
    });

    connection.on("SendMessage", function (message) {
      console.log("SendMessage:" + message);
    });

    connection.on("Push", function (jukeBoxMusic) {
      console.log("Push:" + JSON.stringify(jukeBoxMusic));
    });

    connection
      .start()
      .then(function () {
        console.log("Start");
      })
      .catch(function (err) {
        return console.error(err.toString());
      });

    window.vue = this;
    window.conn = connection;
  },
};
</script>
