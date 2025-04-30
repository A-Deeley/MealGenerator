<script setup lang="ts">
//import { useHouseholdStore } from '@/stores/householdStore'
import type { MemberResponse } from '@/types/backend.interface'

defineProps<{
  member: MemberResponse
}>()

//const householdStore = useHouseholdStore()
</script>

<template>
  <span>{{ member.nickname }}</span>
</template>
