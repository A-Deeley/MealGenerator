<script lang="ts" setup>
import type { HouseholdResponse } from '@/types/backend.interface'

defineProps<{
  household: HouseholdResponse
}>()
</script>

<template>
  <RouterLink :to="'/households/' + household.id" class="item">
    <img class="icon" src="/dist//assets//household_icon.svg" />
    <span>{{ household.name }}</span></RouterLink
  >
</template>

<style lang="css" scoped>
.icon {
  width: 100px;
  color: orange;
  aspect-ratio: 1;
}

.item {
  height: 140px;
  aspect-ratio: 1;
  padding: 1rem;
  display: flex;
  flex-direction: column;
  align-items: center;
  transition: 50ms ease-in-out;
}

.item:hover {
  background-color: #cccccc;
  border-radius: 5px;
  font-weight: bold;
}

.item span {
  width: 100%;
}
</style>
