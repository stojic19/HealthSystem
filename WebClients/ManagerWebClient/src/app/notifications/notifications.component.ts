import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.css']
})
export class NotificationsComponent implements OnInit {
  notifications: any[] = [{id: 1, title: "Naslov notifikacije", description: "opis", date: "19.1.2022."},
  {id: 2, title: "Naslov notifikacije 2", description: "opis2", date: "19.1.2022."}];

  constructor() { }

  ngOnInit(): void {
  }

  delete(id){
    console.log(id);
  }

}
