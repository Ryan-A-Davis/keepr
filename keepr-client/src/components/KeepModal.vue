<template>
  <div class="keepmodal">
    <!-- Modal -->
    <div class="modal fade" :id="'modal' + keepProps.id" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
          <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body container-fluid">
            <div class="row">
              <div class="col-6">
                <img id="profile" class="w-100 h-100" :src="keepProps.img" alt="">
              </div>
              <div class="col-6">
                <div class="row">
                  <div class="col-3">
                    <p class="text-info">
                      Views: {{ keepProps.views }}
                    </p>
                  </div>
                  <div class="col-3">
                    <p class="text-warning">
                      Keeps: {{ keepProps.keeps }}
                    </p>
                  </div>
                </div>
                <div class="row text-center">
                  <h2>{{ keepProps.name }}</h2>
                </div>
                <div class="row text-center">
                  <p>{{ keepProps.description }}</p>
                </div>
              </div>
            </div>
          </div>
          <div class="modal-footer row justify-content-around">
            <div class="col-3">
              <select @change="saveToVault()" v-model="state.selectedVault">
                <option v-for="v in state.vaults" :key="v.id" :value="v.id">
                  {{ v.id }}
                </option>
              </select>
            </div>
            <div class="col-3" v-if="keepProps.creatorId === state.account.id">
              <button class="btn btn-danger" @click="remove()">
                Delete
              </button>
            </div>
            <div class="col-3">
              <img id="profImg" @click="goToProfile(keepProps.creatorId)" :src="keepProps.creator.picture">
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive, onMounted } from 'vue'
import { profilesService } from '../services/ProfilesService'
import { accountService } from '../services/AccountService'
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
import { AppState } from '../AppState'
import { useRouter } from 'vue-router'
import { vaultKeepsService } from '../services/VaultKeepsService'
import NotificationService from '../services/NotificationsService'
export default {
  name: 'KeepModal',
  props: {
    keepProps: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const state = reactive({
      user: computed(() => AppState.user),
      vaults: computed(() => AppState.vaults),
      profile: computed(() => AppState.activeProfile),
      account: computed(() => AppState.account),
      selectedVault: {}
    })
    onMounted(async() => {
      try {
        await profilesService.getById(props.keepProps.creatorId)
        if (state.account.id === state.user.id) {
          await accountService.getVaults()
        }
      } catch (error) {
        logger.error(error)
      }
    })
    const router = useRouter()
    return {
      state,
      async remove() {
        try {
          if (await NotificationService.confirm()) {
            await keepsService.delete(props.keepProps.id)
          }
        } catch (error) {
          logger.error(error)
        }
      },
      async saveToVault() {
        try {
          // state.vaults = await profilesService.getVaults(state.user.id)
          // await vaultKeepsService.create(id, state.vaults[0].id)
          logger.log(state.selectedVault)
          await vaultKeepsService.create(state.selectedVault, props.keepProps.id)
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
.modal-content{
  min-height: 80vh;
  min-width: 60vw;
}

#profImg{
  border-radius: 50%;
  max-height: 40px;
}

</style>
