<!-- events.vue -->
<template>
  <div>
    <div class="ui negative message" v-if="is_loading_error">
      <div class="header">
        {{ error_message }}
      </div>
    </div>
    <div v-else>
      <h1 class="ui horizontal divider header">{{ title }}</h1>
      <div class="ui grid">
        <div class="right floated left aligned six wide column">
          <h4>created: {{ date_time_format(created) }}</h4>
        </div>
        <div class="left floated right aligned six wide column">
          <h4 v-show="expired != null">expired: {{ date_time_format(expired) }}</h4>
        </div>
      </div>
      <div class="ui raised segment">
        <pre>{{ body }}</pre>
      </div>
    </div>
  </div>
</template>

<script>
module.exports = {
  name: "notebyurl",
  data() {
    return {
      title: "",
      body: "",
      created: null,
      expired: null,
      error_message: ""
    }
  },
  methods: {
    date_time_format: function(datetime) {
      if (_.isNil(datetime)) return "";
      return moment(datetime).format("DD/MM/YYYY, HH:mm");
    },
    get_note: function() {
      var self = this;
      self.error_message = "";
      axios.get('/api/notes/' + self.$route.params.shortUrl).then(function(response) {
        console.log(response);
        self.title = response.data.title;
        self.body = response.data.body;
        self.created = response.data.createdDateTime;
        self.expired = response.data.expiredDateTime;
      }).catch(function(error) {
        console.log(error);
        switch (error.response.status) {
          case 423: {
            self.error_message = "Note has been expired";
            break;
          }
          case 404: {
            self.error_message = "Note not found";
            break;
          }
          default:
            self.error_message = "Unknown error";
            break;
        }
      });
    }
  },
  computed: {
    is_loading_error: function() {
      return !_.isEmpty(this.error_message);
    }
  },
  created: function() {
    this.get_note();
  },
};
</script>