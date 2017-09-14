<template>
  <div id="sparoot">

    <div class="ui menu fixed">
      <div class="ui container">
        <router-link to="/" class="header item">PastBinFake</router-link>
        <router-link to="/" active-class="active" v-bind:exact="true" class="item">Latest notes</router-link>
        <router-link to="/notes/create" active-class="active" v-bind:exact="true" class="item">New note</router-link>
      </div>
    </div>

    <div class="ui main container">
      <!-- <transition name="fade"> -->
        <router-view></router-view>
      <!-- </transition> -->
    </div>
  </div>
</template>


<script>
module.exports = {
  name: "BaseRouter"
};
</script>