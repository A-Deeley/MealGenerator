import { initializeApp } from 'firebase/app'
import { getAuth } from 'firebase/auth'

const firebaseConfig = {
  apiKey: 'AIzaSyCXhcff9MrDOaYFytcbGYDQDvTgsXH0v5w',
  authDomain: 'meal-generator-68891.firebaseapp.com',
  projectId: 'meal-generator-68891',
  storageBucket: 'meal-generator-68891.firebasestorage.app',
  messagingSenderId: '519344442645',
  appId: '1:519344442645:web:d5de9b9301ece5b0ad2d1c',
}

const firebase = initializeApp(firebaseConfig)
const auth = getAuth(firebase)

export { firebase, auth }
