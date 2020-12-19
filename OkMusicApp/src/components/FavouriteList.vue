<template>
  <el-card>
    <template #header>
      <h1>我的收藏</h1>
    </template>
    <el-table :data="favouritelist" :show-header="false">
      <el-table-column type="index" width="50"> </el-table-column>
      <el-table-column prop="music.title"> </el-table-column>
      <el-table-column prop="music.duration" :formatter="durationFormatter"> </el-table-column>
      <el-table-column>
        <template #default="scope">
          <el-button @click="chooseMusic(scope.row)" type="text" size="small"
            >点歌&lt;&lt;</el-button
          >
        </template>
      </el-table-column>
    </el-table>
    <el-upload
      class="upload-demo"
      ref="upload"
      action="/api/user/musics/upload"
      :on-preview="handlePreview"
      :on-remove="handleRemove"
      :file-list="uploadlist"
      :auto-upload="false"
    >
      <template #trigger>
        <el-button size="small" type="primary">选取文件</el-button>
      </template>
      <el-button
        style="margin-left: 10px"
        size="small"
        type="success"
        @click="submitUpload"
        >上传到服务器</el-button
      >
      <template #tip>
        <div class="el-upload__tip">只能上传音频文件</div>
      </template>
    </el-upload>
  </el-card>
</template>

<script>
export default {
  name: "FavouriteList",
  props: {},
  data() {
    return {
      favouritelist: [],
      uploadlist: [],
    };
  },
  created() {
    fetch("/api/user/favourites")
      .then((response) => response.json())
      .then((response) => {
        console.log(response);
        this.favouritelist = response;
      });

    this.uploadlist = [];
  },
  methods: {
    durationFormatter(row, column, cellValue, index) {
      var date = new Date(cellValue);
      return date.getMinutes() + ":" + date.getSeconds();
    },
    submitUpload() {
      this.$refs.upload.submit();
    },
    handleRemove(file, uploadlist) {
      console.log(file, uploadlist);
    },
    handlePreview(file) {
      console.log(file);
    },
    chooseMusic(music) {
      window.conn.invoke("Push", music.musicId);
    },
  },
};
</script>