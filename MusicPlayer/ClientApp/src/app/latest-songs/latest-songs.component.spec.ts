import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LatestSongsComponent } from './latest-songs.component';

describe('LatestSongsComponent', () => {
  let component: LatestSongsComponent;
  let fixture: ComponentFixture<LatestSongsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LatestSongsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LatestSongsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
