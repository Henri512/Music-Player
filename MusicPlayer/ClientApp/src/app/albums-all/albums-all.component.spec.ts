import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlbumsAllComponent } from './albums-all.component';

describe('AlbumsAllComponent', () => {
  let component: AlbumsAllComponent;
  let fixture: ComponentFixture<AlbumsAllComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlbumsAllComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlbumsAllComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
