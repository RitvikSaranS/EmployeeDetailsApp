import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root",
})
export class PasswordHasherService {
  constructor() {}

  hashPassword(password: string): string {
    let hash = 0;
    if (password.length === 0) {
      return hash.toString();
    }
    for (let i = 0; i < password.length; i++) {
      const char = password.charCodeAt(i);
      hash = (hash << 5) - hash + char;
      hash = hash & hash;
    }
    return hash.toString();
  }
}
