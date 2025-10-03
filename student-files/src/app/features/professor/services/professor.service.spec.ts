import { TestBed } from '@angular/core/testing';

import { ProfessorServiceService } from './professor.service';

describe('ProfessorServiceService', () => {
  let service: ProfessorServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProfessorServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
