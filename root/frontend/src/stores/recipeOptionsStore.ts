import { type GetRecipeGenOptions, type PostRecipeGenOption } from '@/types/backend.interface'
import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useRecipeOptionsStore = defineStore('recipeOptions', () => {
  const options = ref<GetRecipeGenOptions[]>()

  async function GetOptions(householdId: number) {
    const response = await fetch(`https://localhost:7163/RecipeConfig?householdId=${householdId}`, {
      method: 'get',
      credentials: 'include',
    })

    options.value = await response.json()
  }

  async function PostOptions(option: PostRecipeGenOption) {
    await fetch('https://localhost:7163/RecipeConfig', {
      method: 'post',
      credentials: 'include',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(option),
    })
  }

  return {
    GetOptions,
    PostOptions,
    options,
  }
})
