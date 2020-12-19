<template>
  <el-row :gutter="20">
    <el-col :span="12"><OkHall msg="Hello Vue 3.0 + Vite" /></el-col>
    <el-col :span="12"><FavouriteList /><AllMusic /></el-col>
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

export default {
  name: "App",
  components: {
    OkHall,
    FavouriteList,
    AllMusic,
  },
  created() {},
};
</script>
