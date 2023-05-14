import { TestBed } from '@angular/core/testing';

import { DepartmentRequestsService } from './department-requests.service';

describe('DepartmentRequestsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DepartmentRequestsService = TestBed.get(DepartmentRequestsService);
    expect(service).toBeTruthy();
  });
});
