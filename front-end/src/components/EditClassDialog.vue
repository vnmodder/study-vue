<!-- Dialog.vue -->

<template>
    <div class="dialog">
        <!-- Nội dung của hộp thoại -->
        <div class="dialog-content">
            <h4>Thông tin của {{ item.name }}</h4>
            <!-- Nội dung chỉnh sửa thông tin -->
            <div class="input-group mt-2">
                <span class="input-group-text">Mã lớp</span>
                <input class="form-control" :value="item.code" ref="classCode" />
            </div>
            <div class="input-group">
                <span class="input-group-text">Tên lớp</span>
                <input class="form-control mt-2" :value="item.name" ref="tenLop" />
            </div>
            <div class="d-flex justify-content-end">
                <button class="btn btn-success  mt-2" @click="saveChanges">Save</button>
                <button class="btn btn-danger ms-2  mt-2" @click="closeDialog">Cancel</button>
            </div>
        </div>
    </div>
</template>
  
<script>
import Service from "../commons/service"

export default {
    name: "EditClassDialog",
    props: ["item"],
    data() {
        return {
            newName: "",
            newCode: "",
        }
    },
    methods: {
        async saveChanges() {
            const newClass = { id: this.item.id, code: this.$refs.classCode.value, name: this.$refs.tenLop.value }
            if (newClass.name == "" && newClass.code == "") {
                this.closeDialog();
                return;
            }
            if (newClass.name == "" || newClass.code == "") {
                alert("Hãy nhập đủ Mã lớp và Tên lớp trước khi lưu!")
                return;
            }
            const response = await Service.put("Class", newClass)
            if (response.status == 200) {
                this.$emit('save');
                this.closeDialog();
                return;
            }
            else {
                alert(response.data)
            }
        },
        closeDialog() {
            // Đóng hộp thoại
            this.$emit('close');
        },
    },
};
</script>
  
<style scoped>
/* CSS cho hộp thoại */
.dialog {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background-color: rgba(0, 0, 0, 0.5);
    display: flex;
    justify-content: center;
    align-items: center;
}

.dialog-content {
    background-color: white;
    padding: 20px;
    border-radius: 5px;
}
</style>
  