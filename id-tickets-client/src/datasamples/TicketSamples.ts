
type Ticket = {
    id: number, 
    requestor: string, 
    title: string, 
    dateAdded: Date,
    dateUpdated: Date,
    status: string,
    priority: string;
  };
  

export const ticketArray: Ticket[] = [
    {
      id: 1,
      requestor: "John Doe",
      title: "Cannot access email",
      dateAdded: new Date("2022-02-05"),
      dateUpdated: new Date("2022-02-06"),
      status: "In Progress",
      priority: "High",
    },
    {
      id: 2,
      requestor: "Jane Smith",
      title: "Slow internet connection",
      dateAdded: new Date("2022-02-08"),
      dateUpdated: new Date("2022-02-09"),
      status: "Resolved",
      priority: "Medium",
    },
    {
      id: 3,
      requestor: "David Johnson",
      title: "Printer not working",
      dateAdded: new Date("2022-02-11"),
      dateUpdated: new Date("2022-02-12"),
      status: "In Progress",
      priority: "High",
    },
    {
      id: 4,
      requestor: "Sara Lee",
      title: "Can't login to the system",
      dateAdded: new Date("2022-02-14"),
      dateUpdated: new Date("2022-02-15"),
      status: "Resolved",
      priority: "High",
    },
    {
      id: 5,
      requestor: "Michael Brown",
      title: "Software installation issue",
      dateAdded: new Date("2022-02-17"),
      dateUpdated: new Date("2022-02-18"),
      status: "In Progress",
      priority: "Low",
    },
    {
      id: 6,
      requestor: "Lisa Davis",
      title: "System crash",
      dateAdded: new Date("2022-02-20"),
      dateUpdated: new Date("2022-02-21"),
      status: "Resolved",
      priority: "High",
    },
    {
      id: 7,
      requestor: "Kevin Wilson",
      title: "Email configuration issue",
      dateAdded: new Date("2022-02-23"),
      dateUpdated: new Date("2022-02-24"),
      status: "In Progress",
      priority: "Medium",
    },
    {
      id: 8,
      requestor: "Maggie Chen",
      title: "Need access to shared drive",
      dateAdded: new Date("2022-02-26"),
      dateUpdated: new Date("2022-02-27"),
      status: "Resolved",
      priority: "Low",
    },
    {
      id: 9,
      requestor: "Jason Lee",
      title: "VPN connection issue",
      dateAdded: new Date("2022-03-01"),
      dateUpdated: new Date("2022-03-02"),
      status: "In Progress",
      priority: "High",
    },
    {
      id: 10,
      requestor: "Rachel Kim",
      title: "Need new laptop",
      dateAdded: new Date("2022-03-04"),
      dateUpdated: new Date("2022-03-05"),
      status: "Resolved",
      priority: "High",
    },
  ];
  

