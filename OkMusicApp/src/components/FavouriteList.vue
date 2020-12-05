<template>
  <el-table :data="favouritelist" :show-header="false">
    <el-table-column type="index" width="50"> </el-table-column>
    <el-table-column prop="title"> </el-table-column>
  </el-table>
  <el-upload
    class="upload-demo"
    ref="upload"
    action="api/myfavourite/upload"
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
      <div class="el-upload__tip">只能上传 jpg/png 文件，且不超过 500kb</div>
    </template>
  </el-upload>
</template>

<script>
export default {
  name: "FavouriteList",
  props: {},
  data() {
    return {
      favouritelist: [],
      uploadlist: []
    };
  },
  created() {
    this.uploadlist = [
      {
        id: "2",
        name: "The End of the World",
        url:
          "https://m701.music.126.net/20201204234305/1609e389331a32fa47cd11fd8a2916c7/jdyyaac/015f/565d/0753/65fd473809a77035d120098452a6447f.m4a",
      },
      {
        id: "3",
        name: "生命中最美丽的一天",
        url:
          "https://m10.music.126.net/20201204234336/d7165e1c8ca75cdeefed206d9da9028f/yyaac/obj/wonDkMOGw6XDiTHCmMOi/3058336168/c6bb/4ce4/3ad5/d184f02f3b78e785aff7b9182522e922.m4a",
      },
    ];
    this.favouritelist = [
      {
        id: "1",
        title: "放漾（电影《反转人生》插曲）",
        url:
          "https://m701.music.126.net/20201204234223/b0e7c08f94cbac8f420ab48f56743045/jdyyaac/545b/0e52/010c/c1bc05bab3981c2f5c266f5b9420eafa.m4a",
      },
      {
        id: "2",
        title: "The End of the World",
        url:
          "https://m701.music.126.net/20201204234305/1609e389331a32fa47cd11fd8a2916c7/jdyyaac/015f/565d/0753/65fd473809a77035d120098452a6447f.m4a",
      },
      {
        id: "3",
        title: "生命中最美丽的一天",
        url:
          "https://m10.music.126.net/20201204234336/d7165e1c8ca75cdeefed206d9da9028f/yyaac/obj/wonDkMOGw6XDiTHCmMOi/3058336168/c6bb/4ce4/3ad5/d184f02f3b78e785aff7b9182522e922.m4a",
      },
      {
        id: "4",
        title: "你是人间四月天",
        url:
          "https://m701.music.126.net/20201204234148/dc4310a6c69fb2f0dcb27f16f3c17f1d/jdyyaac/5359/0e5b/0f08/d3d17e41b791880207018ba78a8ad95d.m4a",
      },
    ];
  },
  methods: {
    submitUpload() {
      this.$refs.upload.submit();
    },
    handleRemove(file, uploadlist) {
      console.log(file, uploadlist);
    },
    handlePreview(file) {
      console.log(file);
    },
  },
};
</script>