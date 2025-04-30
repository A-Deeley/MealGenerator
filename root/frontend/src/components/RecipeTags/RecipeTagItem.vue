<script setup lang="ts">
import type { RecipeTagResponse } from '@/types/backend.interface'

defineProps<{
  tag: RecipeTagResponse
}>()
</script>

<template>
  <div
    class="tag"
    :style="{ border: `1px solid ${tag.colour}`, backgroundColor: `${tag.colour}AA` }"
  >
    <span :style="{ margin: '0.2rem' }">{{ tag.name }}</span>
  </div>
</template>

<style scoped lang="css">
.tag {
  width: fit-content;
  border-radius: 5px;
}
</style>
