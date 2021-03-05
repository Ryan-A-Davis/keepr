<template>
  <div class="vault col-3">
    <div class="card rounded drop-shadow">
      <div class="card-body">
        <h4 class="card-title" @click="goToVault(vaultProps.id)">
          {{ vaultProps.name }}
        </h4>
        <button class="btn btn-danger" v-if="vaultProps.creatorId === state.account.id" @click="remove">
          Delete
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { useRouter } from 'vue-router'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import NotificationService from '../services/NotificationsService'
import { logger } from '../utils/Logger'

export default {
  name: 'Vault',
  props: {
    vaultProps: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const router = useRouter()
    const state = reactive({
      account: computed(() => AppState.account)
    })
    return {
      state,
      goToVault(id) {
        router.push({ name: 'VaultDetails', params: { id: id } })
      },
      async remove() {
        try {
          if (await NotificationService.confirm()) {
            await vaultsService.delete(props.vaultProps.id)
          } else {
            alert('changes not saved')
          }
        } catch (error) {
          logger.error(error)
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.card{
  background-image: url("https://image.shutterstock.com/z/stock-photo-front-view-of-light-silver-bank-vault-door-closed-d-render-386619208.jpg") ;
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;

  min-height: 200px;
  min-width: 200px;
}

</style>
