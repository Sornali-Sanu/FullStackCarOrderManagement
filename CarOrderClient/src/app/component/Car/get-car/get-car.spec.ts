import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetCar } from './get-car';

describe('GetCar', () => {
  let component: GetCar;
  let fixture: ComponentFixture<GetCar>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GetCar]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetCar);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
