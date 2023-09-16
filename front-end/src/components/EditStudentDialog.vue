<!-- Dialog.vue -->

<template>
    <div class="dialog">
        <!-- Nội dung của hộp thoại -->
        <div class="dialog-content">
            <h4>Thôn tin của {{ item.name }}</h4>
            <!-- Nội dung chỉnh sửa thông tin -->
            <div class="input-group mt-2">
                <span class="input-group-text">MSSV</span>
                <input class="form-control" :value="item.code" ref="code" />
            </div>
            <div class="input-group mt-2">
                <span class="input-group-text">Tên SV</span>
                <input class="form-control" :value="item.name" ref="name" />
            </div>
            <div class="input-group mt-2">
                <span class="input-group-text">Lớp học</span>
                <select class="form-select" :value="item.class_id" ref="class_id">
                    <option v-for="cl in allClass" :value="cl.id" :key="cl.id + 'cl'">{{ cl.name }}
                    </option>
                </select>
            </div>
            <div class="d-flex justify-content-end mt-2">
                <button class="btn btn-success" @click="saveChanges">Save</button>
                <button class="btn btn-danger ms-2" @click="closeDialog">Cancel</button>
            </div>
        </div>
    </div>
</template>
  
<script>
import Service from "../commons/service"

export default {
    name: "EditStudentDialog",
    props: ["item"],
    data() {
        return {
            newName: "",
            newCode: "",
            allClass: []
        }
    },
    methods: {
        async saveChanges() {
            const newClass = { id: this.item.id, code: this.$refs.code.value, name: this.$refs.name.value, class_id: this.$refs.class_id.value }
            if (newClass.name == "" && newClass.code == "") {
                this.closeDialog();
                return;
            }
            if (newClass.name == "" || newClass.code == "") {
                alert("Hãy nhập đủ MSSV và Tên SV trước khi lưu!")
                return;
            }
            const response = await Service.put("Student", newClass)
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
    async created() {
        const response = await Service.get("Class")
        if (response.status == 200) {
            this.allClass = response.data
            return;
        }
        this.allClass = []
    }
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
  