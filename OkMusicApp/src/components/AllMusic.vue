<template>
  <el-card>
    <template #header>
      <h1>AllMusic</h1>
    </template>
    <el-table :data="musics" :show-header="false">
      <el-table-column type="index" width="50"> </el-table-column>
      <el-table-column prop="title"> </el-table-column>
      <el-table-column prop="duration" :formatter="durationFormatter">
      </el-table-column>
      <el-table-column prop="creator"> </el-table-column>
      <el-table-column>
        <template #default="scope">
          <el-button @click="chooseMusic(scope.row)" type="text" size="small"
            >点歌&lt;&lt;</el-button
          >
        </template>
      </el-table-column>
    </el-table>
  </el-card>
</template>

<script>
export default {
  name: "AllMusic",
  props: {},
  data() {
    return {
      musics: [],
    };
  },
  created() {
    fetch("/api/musics")
      .then((response) => response.json())
      .then((response) => {
        console.log(response);
        this.musics = response;
      });
  },
  methods: {
    durationFormatter(row, column, cellValue, index) {
      var date = new Date(cellValue);
      return date.getMinutes() + ":" + date.getSeconds();
    },
    chooseMusic(music) {
      window.conn.invoke("Push", music.musicId);
    },
  },
};
</script>