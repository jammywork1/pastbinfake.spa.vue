<template>
  <div>
    <div class="ui negative message" v-bind:class="{ hidden: !is_error_message_visible }">
      <div class="header">
        We're sorry, your note can't create
      </div>
      <p>{{ error_message }}</p>
    </div>
    <div class="ui form" v-bind:class="{ loading: is_send_data }">
      <div class="field">
        <div class="fields required">
          <div class="twelve wide field">
            <label for="title">Title</label>
            <input class="form-control" id="title" type="text" placeholder="enter your title here" v-model="title">
          </div>
          <div class="four wide field">
            <label for="duration">Duration</label>
            <select class="form-control" id="duration" v-model="duration">
              <option v-for="dur in duration_enums" v-bind:value="dur.value">{{ dur.label }}</option>
            </select>
          </div>
        </div>

      </div>
      <div class="field required">
        <label>Text</label>
        <textarea rows="20" placeholder="enter your note here" v-model="body"></textarea>
      </div>
      <button class="ui button" v-bind:class="{ disabled: !is_all_field_valid }" v-on:click="create_new_note">Create note</button>
    </div>
  </div>
</template>

<script>
module.exports = {
  name: "create",
  data() {
    return {
      is_send_data: false,
      title: "",
      body: "",
      duration: 0,
      error_message: "",
      duration_enums: [
        { label: "Nothing", value: 0 },
        { label: "10 minutes", value: 1 },
        { label: "1 hour", value: 2 },
        { label: "1 day", value: 3 },
        { label: "1 week", value: 4 },
        { label: "1 month", value: 5 }
      ]
    }
  },
  methods: {
    create_new_note: function() {
      var self = this;
      self.is_send_data = true;
      self.error_message = "";
      axios.post('/api/notes', { Title: self.title, Body: self.body, Duration: self.duration }).then(_.debounce(function(response) {
        console.log(response);
        self.is_send_data = false;
        self.$router.push({ path: '/' })
      }, 1000)).catch(function(error) {
        self.is_send_data = false;
        console.log(error);
        switch (error.response.status) {
          case 406: {
            self.error_message = "Not valid";
            break;
          }
          default: {
            self.error_message = "Unknown error";
            break;
          }

        }

      });
    }
  },
  computed: {
    is_all_field_valid: function() {
      var self = this;
      return !_.isEmpty(self.title) && !_.isEmpty(self.body);
    },
    is_error_message_visible: function() {
      var self = this;
      return !_.isEmpty(self.error_message);
    }
  },
  created: function() {
    self.is_send_data = false;
  }

};
</script>