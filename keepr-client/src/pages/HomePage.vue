<template>
  <div class="container-fluid ">
    <div class="grid-container">
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
      },
      async updateViews(id) {
        try {
          debugger
          const update = await keepsService.getById(id)
          if (state.user !== state.account) {
            update.views += 1
            await keepsService.edit(id, update)
          }
        } catch (error) {
          logger.error(error)
        }
      }
    }
  }
}
</script>

<style scoped lang="scss">
.grid-container {
  column-count: 5;
  column-gap: 20px;

}

.item {
  display: inline-block;
  margin: 15px 15px 15px;
  width: auto;
  box-sizing: border-box;
  border: solid 2px black;
  box-shadow: 5px 5px 5px rgba(0, 0, 0, 0.5);
  border-radius: 5px;
}

</style>
