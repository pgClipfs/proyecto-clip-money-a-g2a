import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ObtenerComponent } from './obtener.component';

describe('ObtenerComponent', () => {
  let component: ObtenerComponent;
  let fixture: ComponentFixture<ObtenerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ObtenerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ObtenerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
