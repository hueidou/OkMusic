<template>
  <!-- <OkHall msg="Hello Vue 3.0 + Vite" /> -->

  <FavouriteList></FavouriteList>
</template>

<script>
import OkHall from './components/OkHall.vue'
import FavouriteList from './components/FavouriteList.vue'

export default {
  name: 'App',
  components: {
    OkHall,
    FavouriteList
  }
}
</script>
