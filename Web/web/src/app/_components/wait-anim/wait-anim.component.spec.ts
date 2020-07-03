/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { WaitAnimComponent } from './wait-anim.component';

describe('WaitAnimComponent', () => {
  let component: WaitAnimComponent;
  let fixture: ComponentFixture<WaitAnimComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WaitAnimComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WaitAnimComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
