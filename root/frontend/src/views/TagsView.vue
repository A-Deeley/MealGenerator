<script setup lang="ts">
import RecipeTagItem from '@/components/RecipeTags/RecipeTagItem.vue'
import { useRecipeStore } from '@/stores/recipeStore'
import type { PostRecipeTagRequest } from '@/types/backend.interface'
import { storeToRefs } from 'pinia'
import { ref } from 'vue'

const recipeStore = useRecipeStore()
recipeStore.getTags()
const { tags } = storeToRefs(recipeStore)
const tagName = ref<string>('')
const tagColour = ref<string>('')

const onRecipeAdded = async () => {
  const request: PostRecipeTagRequest = {
    name: tagName.value,
    colour: tagColour.value,
  }

  await recipeStore.updateTag(request)
  tagName.value = ''
  tagColour.value = ''
}
</script>

<template>
  <div id="tags">
    <h1>Tags management</h1>
    <hr />
    <div class="container">
      <div>
        Name:
        <input type="text" v-model="tagName" />

        Color:
        <input type="color" v-model="tagColour" />
      </div>
      <button @click="onRecipeAdded">Add</button>
    </div>
    <div class="container">
      <RecipeTagItem v-for="tag in tags" :key="tag.id" :tag="tag" />
    </div>
  </div>
</template>

<style lang="css" scoped>
.container {
  display: flex;
  gap: 2rem;
  flex-direction: column;
}

#tags {
  width: 50%;
}
</style>
