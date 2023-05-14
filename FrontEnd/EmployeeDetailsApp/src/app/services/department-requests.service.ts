import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Department } from "../Models/Department";
import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: "root",
})
export class DepartmentRequestsService {
  departments: Observable<Department[]>;
  constructor(private http: HttpClient) {}
  getDepartment() {
    return this.http.get<Department[]>(environment.API_URL + "/department");
  }
  postDepartment() {}
  putDepartment() {}
  deleteDepartment() {}
}
