import { TestBed } from '@angular/core/testing';

import { EmployeeRequestsService } from './employee-requests.service';

describe('EmployeeRequestsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EmployeeRequestsService = TestBed.get(EmployeeRequestsService);
    expect(service).toBeTruthy();
  });
});
