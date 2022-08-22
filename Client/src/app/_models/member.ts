import { photo } from "./photo"

export interface member {
    id: string
    userName: string
    knownAs: string
    profileUrl: string
    created: string
    lastActive: string
    gender: string
    age: number
    introduction: string
    lookingFor: string
    interests: string
    city: string
    country: string
    photos: photo[]
  }