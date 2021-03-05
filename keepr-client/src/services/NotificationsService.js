import Swal from 'sweetalert2'
import { logger } from '../utils/Logger'
export default class NotificationService {
  static async confirm(title = 'Are you sure?', text = "You won't be able to reverse this action!") {
    try {
      const res = await Swal.fire({
        title: title,
        text: text,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Delete it!'
      })
      logger.log(res)
      if (res.isConfirmed) {
        return true
      }
      return false
    } catch (error) {
      logger.error(error)
    }
  }
}
