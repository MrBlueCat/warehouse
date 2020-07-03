<template>
  <div>
    <form name="addForm" @submit.prevent="createItem()">
      <span class="title requiredTitle">Item name:</span>
      <input type="text" v-model="item.name" placeholder="Input name" class="input" required />

      <span class="title requiredTitle">Manufacturer:</span>
      <select v-model.number="item.manufacturerId" class="input" required>
        <option
          v-for="manufacturer in manufacturers"
          v-bind:key="manufacturer.id"
          v-bind:value="manufacturer.id"
        >{{manufacturer.name}}</option>
      </select>

      <span class="title requiredTitle">Customer:</span>
      <select v-model.number="item.customerId" class="input" required>
        <option
          v-for="customer in customers"
          v-bind:key="customer.id"
          v-bind:value="customer.id"
        >{{customer.name}}</option>
      </select>

      <button type="submit" class="btn_sub">Add</button>
    </form>
  </div>
</template>

<script>
import RequestService from "./Core/RequestService";

export default {
  methods: {
    createItem() {
      RequestService.post('items', this.item).then(responce=>{
        this.$emit('create', responce.data);
      });
    },
  },

  data() {
    return {
      item: {
        name: "",
        manufacturerId: null,
        customerId: null
      },
      manufacturers: [],
      customers: [],
      m:"fffff"
    };
  },

  created() {
    RequestService.getMany("manufacturers").then(responce => {
      this.manufacturers = responce.data;
    });

    RequestService.getMany("customers").then(responce => {
      this.customers = responce.data;
    });
  }
};
</script>

<style>

</style>
