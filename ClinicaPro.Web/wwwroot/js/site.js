// site.js - ClinicaPro (sidebar colapsável, mobile, persistência)
(function () {
  "use strict";

  const body = document.body;
  const appSidebar = document.getElementById("appSidebar");
  const sidebarToggle = document.getElementById("sidebarToggle");
  const sidebarCollapseBtn = document.getElementById("sidebarCollapse");
  const STORAGE_KEY = "clinicapro_sidebar_state";

  // Helpers
  function isCollapsed() {
    return appSidebar.classList.contains("collapsed");
  }
  function setCollapsed(collapsed) {
    if (collapsed) {
      appSidebar.classList.add("collapsed");
      // shift content by CSS sibling combinator
    } else {
      appSidebar.classList.remove("collapsed");
    }
    try { localStorage.setItem(STORAGE_KEY, collapsed ? "1" : "0"); } catch (e) {}
  }

  // init state from storage
  (function init() {
    if (!appSidebar) return;
    const stored = (function () { try { return localStorage.getItem(STORAGE_KEY); } catch(e){ return null;} })();
    if (stored === "1") { appSidebar.classList.add("collapsed"); }
  })();

  // toggle by topbar button
  if (sidebarToggle) {
    sidebarToggle.addEventListener("click", function (e) {
      e.preventDefault();
      if (!appSidebar) return;
      // on mobile, open overlay
      if (window.innerWidth < 992) {
        appSidebar.classList.toggle("open");
      } else {
        const collapsed = !isCollapsed();
        setCollapsed(collapsed);
      }
    });
  }

  // mobile close
  if (sidebarCollapseBtn) {
    sidebarCollapseBtn.addEventListener("click", function (e) {
      e.preventDefault();
      appSidebar.classList.remove("open");
    });
  }

  // close overlay if clicked outside (mobile)
  document.addEventListener("click", function (e) {
    if (!appSidebar) return;
    if (window.innerWidth < 992 && appSidebar.classList.contains("open")) {
      const withinSidebar = e.target.closest && e.target.closest("#appSidebar");
      const toggleClicked = e.target.closest && e.target.closest("#sidebarToggle");
      if (!withinSidebar && !toggleClicked) {
        appSidebar.classList.remove("open");
      }
    }
  });

  // Accessibility: close with Escape
  document.addEventListener("keydown", function (e) {
    if (e.key === "Escape" && appSidebar && appSidebar.classList.contains("open")) {
      appSidebar.classList.remove("open");
    }
  });

})();
