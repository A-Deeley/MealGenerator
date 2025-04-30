import type { PostRecipeTagRequest, RecipeTagResponse } from '@/types/backend.interface'
import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useRecipeStore = defineStore('recipes', () => {
  const tags = ref<RecipeTagResponse[]>([])

  async function getTags() {
    const response = await fetch('https://localhost:7163/RecipeTags', {
      method: 'get',
      credentials: 'include',
    })

    tags.value = await response.json()
  }

  async function updateTag(request: PostRecipeTagRequest) {
    await fetch('https://localhost:7163/RecipeTags', {
      method: 'post',
      credentials: 'include',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(request),
    })
    await getTags()
  }

  return {
    tags,
    getTags,
    updateTag,
  }
})
