import { ref } from 'vue'
import { defineStore } from 'pinia'
import {
  type MemberResponse,
  type SignUpPostRequest,
  type UserDetailsResponse,
} from '@/types/backend.interface'
import { auth } from './firebase'
import { signInWithEmailAndPassword, signOut, type UserCredential } from 'firebase/auth'
import { FirebaseError } from 'firebase/app'

export const useUserStore = defineStore('user', () => {
  const isAuthenticated = ref(!!sessionStorage.getItem('auth'))
  const user = ref<UserDetailsResponse>()
  const loading = ref(false)
  const error = ref('')

  const createUser = async (args: SignUpPostRequest) => {
    loading.value = true
    const json = JSON.stringify(args)
    await fetch('https://localhost:7163/Access/SignUp/Email', {
      method: 'post',
      body: json,
      headers: {
        'Content-Type': 'application/json',
      },
    })

    loading.value = false
  }

  async function login(email: string, password: string) {
    loading.value = true
    let signInResponse: UserCredential
    try {
      signInResponse = await signInWithEmailAndPassword(auth, email, password)
    } catch (e) {
      if (e instanceof FirebaseError) {
        error.value = e.message
      }

      return
    } finally {
      loading.value = false
    }

    if (signInResponse.user) {
      const body = { idToken: await signInResponse.user.getIdToken() }
      const backendSignInResponse = await fetch('https://localhost:7163/Access/Login', {
        method: 'post',
        body: JSON.stringify(body),
        headers: {
          'Content-Type': 'application/json',
        },
        credentials: 'include',
      })
      if (backendSignInResponse.status === 200) {
        sessionStorage.setItem('auth', 'true')
        isAuthenticated.value = true
        await signOut(auth)
        await loadUser()
      }
    } else {
      loading.value = false
      throw Error('Error authenticating user')
    }

    loading.value = false
  }

  async function logout() {
    loading.value = true
    await fetch('https://localhost:7163/Access/Logout', { method: 'post', credentials: 'include' })
    isAuthenticated.value = false
    sessionStorage.removeItem('auth')
    user.value = undefined
    loading.value = false
  }

  async function find(search: string): Promise<MemberResponse[]> {
    loading.value = true

    const response = await fetch(`https://localhost:7163/Users/Search?search=${search}`, {
      method: 'get',
      credentials: 'include',
    })

    loading.value = false

    return await response.json()
  }

  async function loadUser() {
    const response = await fetch('https://localhost:7163/Users', {
      method: 'get',
      credentials: 'include',
    })

    user.value = await response.json()
  }

  return { isAuthenticated, createUser, login, logout, loading, error, find, user, loadUser }
})
