import { AppState } from '../AppState'
import { api } from './AxiosService'
import { logger } from '../utils/Logger'

class ProfileService {
  async getById(id) {
    const res = await api.get('api/profiles/' + id)
    logger.log(res)
    return res.data
  }

  async getVaults(id) {
    const res = await api.get('api/profiles/' + id + '/vaults')
    logger.log(res)
    AppState.uservaults = res.data
  }
}
export const profilesService = new ProfileService()
