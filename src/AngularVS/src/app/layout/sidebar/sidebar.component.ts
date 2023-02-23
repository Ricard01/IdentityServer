import { Component, EventEmitter, Input, OnInit, Output, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class SidebarComponent implements OnInit {
  @Input() showToggle = true;
  @Input() toggleChecked = false;

  @Output() toggleCollapsed = new EventEmitter<void>();


  constructor() { }

  ngOnInit(): void {
  }
}
