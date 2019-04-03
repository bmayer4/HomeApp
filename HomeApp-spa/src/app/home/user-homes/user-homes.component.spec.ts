/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { UserHomesComponent } from './user-homes.component';

describe('UserHomesComponent', () => {
  let component: UserHomesComponent;
  let fixture: ComponentFixture<UserHomesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserHomesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserHomesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
