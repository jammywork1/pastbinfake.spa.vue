<template>
  <div>
    <div class="ui negative message" v-if="is_loading_error">
      <div class="header">
        We're sorry, loading error
      </div>
      <p>{{ error_message }}</p>
    </div>
    <div v-else-if="is_notes_empty">
      <h1>Sorry, we don't have notes.</h1> 
    </div>
    <div v-else>
      <div class="ui">
        <div class="ui">
          <div class="ui indeterminate text loader">Preparing Files</div>
        </div>

        <table class="ui selectable very basic table">
          <thead>
            <tr>
              <th class="twelve wide">Title</th>
              <th class="four wide">Created</th>
            </tr>

          </thead>
          <tbody>
            <tr v-for="note in notes">
              <td>
                <router-link v-bind:to="note.url">{{ note.title }}</router-link>
              </td>
              <td>{{ date_time_format(note.createdDateTime) }}</td>

            </tr>

          </tbody>
        </table>
      </div>
    </div>

  </div>
</template>

<script>

module.exports = {
  name: "Home",
  data() {
    return {
      error_message: "",
      notes: []
    }
  },
  computed: {
    is_notes_empty: function() {
      return _.size(this.notes) === 0;
    },
    is_loading_error: function() {
      return !_.isEmpty(this.error_message);
    }
  },
  methods: {
    date_time_format: function(datetime) {
      return moment(datetime).format("DD/MM/YYYY, HH:mm");
    },
    get_all_notes: function() {
      var self = this;
      self.error_message = "";
      axios.get('/api/notes').then(function(response) {
        console.log(response);
        self.notes = _.orderBy(response.data, ['createdDateTime'], ['desc']);
      }).catch(function(error) {
        self.error_message = "Error code:" + error.response.code;
      });
    }
  },
  created: function() {
    this.get_all_notes();
  }
};
</script>