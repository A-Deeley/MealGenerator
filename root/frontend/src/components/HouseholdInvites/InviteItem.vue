<script lang="ts" setup>
import { useHouseholdStore } from '@/stores/householdStore'
import { useUserStore } from '@/stores/userStore'
import type { HouseholdUserInvite } from '@/types/backend.interface'

const props = defineProps<{
  invite: HouseholdUserInvite
}>()

const householdStore = useHouseholdStore()
const userStore = useUserStore()

const onAccept = async () => {
  const { household, id } = props.invite
  await householdStore.updateInvitation(household.id, id, true)
  await userStore.loadUser()
}

const onDecline = async () => {
  const { household, id } = props.invite
  await householdStore.updateInvitation(household.id, id, false)
  await userStore.loadUser()
}
</script>

<template>
  <div class="container">
    <h3>{{ invite.household.name }}</h3>
    <button @click="onAccept">Accept</button>
    <button @click="onDecline">Decline</button>
  </div>
</template>

<style lang="css" scoped>
.container {
  display: flex;
  gap: 2rem;
}
</style>
