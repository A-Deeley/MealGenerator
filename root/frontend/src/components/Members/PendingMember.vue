<script lang="ts" setup>
import type { HouseholdUserInvite } from '@/types/backend.interface'

defineProps<{
  member: HouseholdUserInvite
}>()
</script>

<template>
  <span>{{ member.user.nickname }}</span>
</template>
