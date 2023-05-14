import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Employee } from "../Models/Employee";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: "root",
})
export class EmployeeRequestsService {
  employees: Observable<Employee[]>;
  headers: HttpHeaders = new HttpHeaders();
  constructor(private http: HttpClient) {}
  getEmployee(): Observable<Employee[]> {
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.get<Employee[]>(
      environment.API_URL + "/employee",
      httpOptions
    );
  }
  postEmployee(emp: Employee): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post(
      environment.API_URL + "/employee",
      JSON.stringify(emp),
      httpOptions
    );
  }
  putEmployee(emp: Employee): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    console.log(environment.API_URL + "/employee/" + +emp.empid.substring(3));
    console.log(JSON.stringify(emp));
    return this.http.put(
      environment.API_URL + "/employee/" + +emp.empid.substring(3),
      JSON.stringify(emp),
      httpOptions
    );
  }
  deleteEmployee(id: string): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.delete(
      `${environment.API_URL}/employee/${+id.substring(3)}`,
      httpOptions
    );
  }
}
