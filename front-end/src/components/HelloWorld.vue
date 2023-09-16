<template>
  <div class="row d-flex justify-content-center">
    <div class="col-md-8 content">
      <h1 class="text-center text-primary">Demo VueJS</h1>

      <div class="row d-flex justify-content-start">
        <!-- Các lớp hiện có -->
        <div class="row">
          <h4>Các lớp hiện có</h4>
          <table class="mt-2 table-sm table mx-3 table-striped table-info table-hover table-bordered">
            <thead class="text-center t-header">
              <th scope="col">STT</th>
              <th scope="col">Mã lớp</th>
              <th scope="col">Tên lớp</th>
              <th scope="col">Chức năng</th>
            </thead>
            <tbody>
              <tr v-for="(item, i) in classList" :key="item.id">
                <th class="text-center">{{ i + 1 }}</th>
                <td>{{ item.code }}</td>
                <td>{{ item.name }}</td>
                <td class="d-flex justify-content-center">
                  <button @click="showClassDetail(item)" class="btn btn-info">DS lớp</button>
                  <button class="ms-2 btn btn-warning" @click="openClassEditDialog(item)">Sửa</button>
                  <button class="ms-2 btn btn-danger" @click="removeClass(item)">Xóa</button>
                </td>
              </tr>
              <tr>
                <th class="text-center">{{ classList.length + 1 }}</th>
                <td><input class="form-control" type="text" v-model="newClass.code" placeholder="Mã lớp" /></td>
                <td><input class="form-control" type="text" v-model="newClass.name" placeholder="Tên lớp" /></td>
                <td class="d-flex justify-content-center">
                  <button v-on:click="addNewClass" class="btn btn-success">Thêm lớp mới</button>
                </td>
              </tr>
            </tbody>
          </table>
          <!-- Sử dụng component dialog -->
          <EditClassDialog :item="selectedClass" v-on:save="saveEditedInfo" v-on:close="closeEditedDialog"
            v-if="isEditDialogVisible" :key="selectedClass.id">
          </EditClassDialog>
        </div>
        <!-- Sinh viên trong lớp -->
        <div class="row mt-5" v-if="studentIsVisibled">
          <div class="d-flex justify-content-between">
            <h4>Sinh viên của {{ className }}</h4>
            <button class="btn btn-primary" @click="hideList">Ẩn danh sách</button>
          </div>
          <table class="mt-3 table mx-3 table-striped table-info table-hover table-bordered">
            <thead class="text-center t-header ">
              <th>STT</th>
              <th>MSSV</th>
              <th>Tên Sinh Viên</th>
              <th>Lớp học</th>
              <th>Chức năng</th>
            </thead>
            <tbody>
              <tr v-for="(e, i) in studentList" :key="e.id">
                <th class="text-center">{{ i + 1 }}</th>
                <td>{{ e.code }}</td>
                <td>{{ e.name }}</td>
                <td>{{ className }}</td>
                <td class="d-flex justify-content-center">
                  <button v-on:click="openStudentEditDialog(e)" class="btn btn-warning">Sửa</button>
                  <button class="ms-2 btn btn-danger" @click="removeStudent(e)">Xóa</button>
                </td>
              </tr>
              <tr>
                <th class="text-center">{{ studentList.length + 1 }}</th>
                <td><input class="form-control" type="text" v-model="newStudent.code" placeholder="MSSV" /></td>
                <td><input class="form-control" type="text" v-model="newStudent.name" placeholder="Tên SV" /></td>
                <td>{{ className }}</td>
                <td class="d-flex justify-content-center">
                  <button @click="addNewStudent" class="btn btn-success">Thêm sinh viên</button>
                </td>
              </tr>
            </tbody>
          </table>
          <EditStudentDialog :item="selectedStudent" v-on:save="saveStudentEditedInfo"
            v-on:close="closeStudentEditedDialog" v-if="isStudentEditDialogVisible" :key="selectedStudent.id">
          </EditStudentDialog>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Service from "../commons/service"
import EditClassDialog from './EditClassDialog.vue';
import EditStudentDialog from './EditStudentDialog.vue';

export default {
  name: 'HelloWorld',
  components: {
    EditClassDialog,
    EditStudentDialog,
  },
  data() {
    return {
      studentIsVisibled: false,
      classList: [],
      newClass: { id: 0, code: "", name: "" },
      studentList: [],
      newStudent: { id: 0, code: "", name: "", class_id: 0 },
      className: "Lớp 11A1",
      class_id: 0,
      selectedClass: {},
      isEditDialogVisible: false,
      selectedStudent: {},
      isStudentEditDialogVisible: false,
    }
  },
  methods: {
    async showClassDetail(item) {
      if (!item) {
        this.studentList = []
      }
      this.className = item.name
      this.class_id = item.id
      this.studentIsVisibled = true;
      this.getStudentList(item.id)
    },
    async getStudentList(id) {
      const response = await Service.get("Student/Class/" + id)
      if (response.status == 200) {
        this.studentList = response.data
      } else {
        this.studentList = []
      }
    },
    openClassEditDialog(item) {
      this.selectedClass = item;
      this.isEditDialogVisible = true;
    },
    saveEditedInfo() {
      this.getAllClass();
    },
    closeEditedDialog() {
      this.isEditDialogVisible = false;
    },
    openStudentEditDialog(item) {
      this.selectedStudent = item;
      this.isStudentEditDialogVisible = true;
    },
    saveStudentEditedInfo() {
      this.getStudentList(this.class_id);
    },
    closeStudentEditedDialog() {
      this.isStudentEditDialogVisible = false;
    },
    async addNewClass() {
      if (this.newClass.name == "" || this.newClass.code == "") {
        alert("Phải nhập Mã lớp và Tên lớp!")
        return;
      }
      const response = await Service.post('Class', this.newClass)
      if (response.status == 200) {
        this.newClass = { id: 0, code: "", name: "" }
        this.getAllClass();
      } else {
        alert(response.data)
        return;
      }
    },
    async removeClass(item) {
      if (!item) {
        return;
      }
      alert("Bạn muốn xóa " + item.name);
      const response = await Service.delete("Class/" + item.id)
      if (response.status == 200) {
        this.getAllClass()
      } else {
        alert(response.data)
      }
    },
    async getAllClass() {
      const response = await Service.get("Class")
      if (response.status == 200) {
        this.classList = response.data
      } else {
        this.classList = []
      }
    },
    async addNewStudent() {
      if (this.newStudent.name == "" || this.newStudent.code == "") {
        alert("Phải nhập MSSV và Tên sinh viên!")
        return;
      }
      this.newStudent.class_id = this.class_id;
      const response = await Service.post('Student', this.newStudent)
      if (response.status == 200) {
        this.newStudent = { id: 0, code: "", name: "", class_id: 0 },
          this.getStudentList(this.class_id);
      } else {
        alert(response.data)
        return;
      }
    },
    async removeStudent(item) {
      if (!item) {
        return;
      }
      alert("Bạn muốn xóa sinh viên " + item.name);
      const response = await Service.delete("Student/" + item.id)
      if (response.status == 200) {
        this.getStudentList(this.class_id);
      } else {
        alert(response.data)
      }
    },
    hideList() {
      this.studentIsVisibled = false;
    },
  }
  ,
  created() {
    this.getAllClass()
  }
}


</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.content {
  background-color: #edfaf9e7;
  height: 100vh;
}



.t-header {
  background-color: #0dcaf0;
  height: 3rem;
  vertical-align: middle;
}
</style>
