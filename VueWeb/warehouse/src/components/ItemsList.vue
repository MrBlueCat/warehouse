<template>
  <div class="itemsList">
    <table>
      <thead>
        <tr>
          <th v-for="name in displayedColumns" v-bind:key="name">{{name}}</th>
          <th style="width: 25px;" v-if='auth.isAdmin()'></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in items" v-bind:key="item.id">
          <td>{{item.name}}</td>
          <td>{{item.manufacturer? item.manufacturer.name : ''}}</td>
          <td>{{item.customer? item.customer.name : ''}}</td>
          <td>{{item.date}}</td>
          <td v-if='auth.isAdmin()'>
            <button v-on:click="deleteItem(item)" class="delButt">X</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import RequestService from "./Core/RequestService";
import AuthService from "./Core/Auth";

export default {
  methods: {
    addItem(item) {
      this.items.push(item);
    },

    deleteItem(item) {
      RequestService.delete(`items`, item.id).then(() => {
        const index = this.items.indexOf(item);

        if (index > -1) {
          this.items.splice(index, 1);

        }
      });
    }
  },

  data() {
    return {
      displayedColumns: ["Name", "Manufacturer", "Customer", "Date"],
      items: [],
      auth: AuthService
    };
  },

  created() {
    RequestService.getMany(`items`).then(responce => {
      this.items = responce.data;
    });
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
table,
tr,
td {
  border: 1px solid black;
  border-collapse: collapse;
}

.delButt {
  width: 25px;
  height: 100%;
  font-weight: bold;
  color: white;
  background-color: rgb(245, 4, 4);
  border: none;
}

table {
  width: 80%;
  word-wrap: break-word;
  margin: 0 10% 0 10%;
  text-align: left;
}

tr {
  height: 30px;
}

th {
  background-color: #313131;
  color: white;
  font-weight: bold;
  height: 40px;
}
</style>
