/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { FavHomesComponent } from './fav-homes.component';

describe('FavHomesComponent', () => {
  let component: FavHomesComponent;
  let fixture: ComponentFixture<FavHomesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FavHomesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FavHomesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
