import { Component, OnInit } from "@angular/core";
import { EmployeeRequestsService } from "../services/employee-requests.service";
import { FormGroup, FormControl, Validators, NgForm } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { DepartmentRequestsService } from "../services/department-requests.service";
import { Department } from "../Models/Department";
import { Employee } from "../Models/Employee";

@Component({
  selector: "app-editemployee",
  templateUrl: "./editemployee.component.html",
  styleUrls: ["./editemployee.component.css"],
})
export class EditemployeeComponent implements OnInit {
  public employeeList: Employee[];
  deptList: Department[];
  editForm: FormGroup;
  empId: string;
  employee: Employee;
  currentEditingEmployee: Employee = {
    empid: null,
    name: null,
    age: null,
    gender: null,
    dob: null,
    salary: null,
    address: null,
    deptId: null,
    createdBy: null,
    createdDate: null,
  };
  constructor(
    private empreq: EmployeeRequestsService,
    private deptreq: DepartmentRequestsService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.editForm = new FormGroup({
      name: new FormControl("", Validators.required),
      age: new FormControl("", Validators.required),
      gender: new FormControl("", Validators.required),
      dob: new FormControl("", Validators.required),
      salary: new FormControl("", Validators.required),
      address: new FormControl("", Validators.required),
      department: new FormControl("", Validators.required),
      createdBy: new FormControl("", Validators.required),
    });
    this.empreq.getEmployee().subscribe((emplistitem) => {
      this.employeeList = emplistitem;
      if (emplistitem !== undefined) {
        this.deptreq.getDepartment().subscribe((deplistitem) => {
          this.deptList = deplistitem;
          if (deplistitem !== undefined) {
            this.employee = this.employeeList.filter(
              (x) => x.empid == this.empId
            )[0];
            this.empId = this.route.snapshot.paramMap.get("empid");
            this.employee = this.employeeList.filter(
              (x) => x.empid == this.empId
            )[0];
            this.currentEditingEmployee.empid = this.employee.empid;
            this.currentEditingEmployee.createdDate = this.employee.createdDate;
            for (let i = 0; i < this.deptList.length; i++) {
              if (this.employee.deptId == this.deptList[i].deptId) {
                this.editForm.patchValue({
                  department: this.deptList[i].deptName,
                });
              }
            }
            this.editForm.patchValue({
              name: this.employee.name,
              age: this.employee.age,
              gender: this.employee.gender,
              dob: JSON.stringify(this.employee.dob).slice(1, 11),
              salary: this.employee.salary,
              address: this.employee.address,
              createdBy: this.employee.createdBy,
            });
          }
        });
      }
    });
  }

  onSubmit(form: NgForm) {
    this.currentEditingEmployee.name = this.editForm.value.name;
    this.currentEditingEmployee.age = +this.editForm.value.age;
    this.currentEditingEmployee.gender = this.editForm.value.gender;
    this.currentEditingEmployee.dob = this.editForm.value.dob;
    this.currentEditingEmployee.salary = this.editForm.value.salary;
    this.currentEditingEmployee.address = this.editForm.value.address;
    this.deptList.forEach((dept) => {
      if (dept.deptName == this.editForm.value.department) {
        this.currentEditingEmployee.deptId = dept.deptId;
      }
    });
    this.currentEditingEmployee.createdBy = this.editForm.value.createdBy;
    this.empreq.putEmployee(this.currentEditingEmployee).subscribe(
      () => {
        alert("Employee Updated Successfully");
        this.empreq
          .getEmployee()
          .subscribe((item) => (this.employeeList = item));
      },
      (err) => {
        console.log(err.message);
      }
    );
    form.reset();
    this.router.navigate(["/home"]);
  }
}
