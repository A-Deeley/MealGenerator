<script lang="ts" setup>
import HouseholdItem from '@/components/Household/HouseholdItem.vue'
import { useHouseholdStore } from '@/stores/householdStore'
import { ref } from 'vue'

const householdStore = useHouseholdStore()
householdStore.getHouseholds()
const newHouseholdName = ref('')

const addHousehold = async () => {
  await householdStore.addHousehold(newHouseholdName.value)
  newHouseholdName.value = ''
}
</script>

<template>
  <div v-if="!householdStore.userHouseholds">Loading...</div>
  <div class="parent" v-else>
    <h3>Owner of</h3>
    <hr />
    <div class="household-list">
      <HouseholdItem
        v-for="household in householdStore.userHouseholds.ownerOf"
        :key="household.id"
        :household="household"
      />
    </div>
    <h3>Member of</h3>
    <hr />
    <div class="household-list">
      <HouseholdItem
        v-for="household in householdStore.userHouseholds.memberOf"
        :key="household.id"
        :household="household"
      />
    </div>
    <input type="text" v-model="newHouseholdName" />
    <button @click="addHousehold" :disabled="newHouseholdName.length <= 0">Add household</button>
  </div>
</template>

<style lang="css" scoped>
.parent {
  max-width: 40rem;
}

.household-list {
  display: flex;
  gap: 2rem;
  margin-block: 1rem;
  max-width: 50%;
  flex-wrap: wrap;
}
</style>
