<template>
  <el-row :gutter="20">
    <el-col :span="12">
      <OkHall msg="Hello Vue 3.0 + Vite" />
      <!-- <Chat /> -->
    </el-col>
    <el-col :span="12">
      <FavouriteList />
      <AllMusic style="margin-top: 20px" />
    </el-col>
  </el-row>
  <el-row :gutter="20">
    <el-col :span="12"></el-col>
    <el-col :span="12"></el-col>
  </el-row>
</template>

<script>
import OkHall from "./components/OkHall.vue";
import FavouriteList from "./components/FavouriteList.vue";
import AllMusic from "./components/AllMusic.vue";
import Chat from "./components/Chat.vue";

export default {
  name: "App",
  components: {
    OkHall,
    FavouriteList,
    AllMusic,
    Chat,
  },
  created() {},
};
</script>
