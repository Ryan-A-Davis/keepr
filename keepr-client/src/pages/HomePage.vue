<template>
  <div class="container-fluid">
    <div class="row justify-content-around masonry">
      <Keep v-for="keep in state.keeps" :key="keep.id" :keep-props="keep" :class="'item'" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
// import { useRouter } from 'vue-router'
import { logger } from '../utils/Logger'
export default {
  name: 'Home',
  setup() {
    // const router = useRouter()
    const state = reactive({
      keeps: computed(() => AppState.keeps),
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      newKeep: {},
      keep: computed(() => AppState.activeKeep)
    })
    onMounted(async() => {
      try {
        await keepsService.getAll()
      } catch (error) {
        logger.error(error)
      }
    })
    return {
      state,
      async create() {
        try {
          await keepsService.create(state.newKeep)
        } catch (error) {
          logger.error(error)
        }
      }
    }
  }
}
</script>

<style scoped lang="scss">
.masonry {
  column-count: 3;
  column-gap: 10px;
  display: flex;
  justify-content: center;
  align-self: center;
  min-height: 100vh;
}

.item {
  background-color: #eee;
  display: inline-block;
  margin: 0 0 1em;
  width: 100%;
}

</style>
