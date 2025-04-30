<script setup lang="ts">
import InviteItem from '@/components/HouseholdInvites/InviteItem.vue'
import { useUserStore } from '@/stores/userStore'
import { storeToRefs } from 'pinia'

const userStore = useUserStore()
const { user } = storeToRefs(userStore)
</script>

<template>
  <div>
    <div v-if="user?.pendingInvites">
      <h3>Pending invites</h3>
      <InviteItem v-for="invite in user.pendingInvites" :invite="invite" :key="invite.id" />
    </div>
  </div>
</template>
