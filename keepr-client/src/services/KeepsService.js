import { AppState } from '../AppState'
import { api } from './AxiosService'

class KeepsService {
  async getAll() {
    const res = await api.get('api/keeps')
    console.log(res)
    AppState.keeps = res.data
  }

  async getById(id) {
    const res = await api.get('api/keeps/' + id)
    console.log(res)
    AppState.activeKeep = res.data
  }

  async create(keepData) {
    const res = await api.post('api/keeps', keepData)
    console.log(res)
  }

  async edit(keepId, edited) {
    const toUpdate = edited
    const res = await api.put('api/keeps/' + keepId, toUpdate)
    const keepInd = AppState.keeps.findIndex(k => k.id === keepId)
    AppState.keeps.splice(keepInd, 1, res.data)
  }

  async delete(keepId) {
    await api.delete('api/keeps/' + keepId)
    const keepInd = AppState.keeps.findIndex(k => k.id === keepId)
    AppState.keeps.splice(keepInd, 1)
  }
}

export const keepsService = new KeepsService()
