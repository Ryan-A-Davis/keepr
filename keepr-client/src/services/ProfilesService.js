import { AppState } from '../AppState'
import { api } from './AxiosService'
import { logger } from '../utils/Logger'

class ProfileService {
  async getById(id) {
    const res = await api.get('api/profiles/' + id)
    logger.log(res)
    AppState.activeProfile = res.data
    return res.data
  }

  async getVaults(id) {
    const res = await api.get('api/profiles/' + id + '/vaults')
    logger.log(res)
    AppState.vaults = res.data
  }

  async getKeeps(id) {
    const res = await api.get('api/profiles/' + id + '/keeps')
    logger.log(res)
    AppState.keeps = res.data
  }
}
export const profilesService = new ProfileService()
