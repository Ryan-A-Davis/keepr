<template>
  <div class="keepmodal">
    <!-- Modal -->
    <div class="modal fade" :id="'modal' + keepProps.id" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">
              {{ keepProps.name }}
            </h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <div class="row justify-content-between">
              <div class="col-6">
                <img :src="keepProps.img" alt="">
              </div>
              <div class="col-6">
                <h2>{{ keepProps.name }}</h2>
                <p>{{ keepProps.description }}</p>
              </div>
            </div>
          </div>
          <div class="modal-footer row justify-content-around">
            <div class="col-3">
              <button class="btn btn-primary" @click="savetoVault(keepProps.id)">
                Save to Vault
              </button>
            </div>
            <div class="col-3">
              <img class="profile-img rounded" @click="goToProfile(keepProps.creatorId)" :src="keepProps.creator.picture">
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
// import { vaultsService } from '../services/VaultsService'
import { profilesService } from '../services/ProfilesService'
import { keepsService } from '../services/KeepsService'
import { vaultKeepsService } from '../services/VaultKeepsService'
import { logger } from '../utils/Logger'
import { AppState } from '../AppState'
import { useRouter } from 'vue-router'
export default {
  name: 'KeepModal',
  props: {
    keepProps: {
      type: Object,
      required: true
    }
  },
  setup() {
    const state = reactive({
      user: computed(() => AppState.user),
      vaults: computed(() => AppState.userVaults),
      profile: computed(() => AppState.activeProfile)
    })
    const router = useRouter()
    return {
      state,
      async remove(id) {
        try {
          await keepsService.delete(id)
        } catch (error) {
          logger.error(error)
        }
      },
      async savetoVault(id) {
        try {
          state.vaults = await profilesService.getVaults(state.user.id)
          await vaultKeepsService.create(id, state.vaults[0].id)
        } catch (error) {
          logger.error(error)
        }
      },
      async goToProfile(id) {
        await profilesService.getById(id)
        router.push({ name: 'Profile', params: { id: state.profile.id } })
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
.img{
  min-height: 25px;
}
.modal-body{
  min-height: 60vh;
  min-width: 300px;
}

</style>
