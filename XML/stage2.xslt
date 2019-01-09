<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns="http://www.w3.org/1999/xhtml">

  <xsl:output method="xml" encoding="utf-8" indent="yes" omit-xml-declaration="yes"/>

  <xsl:template match="/root">
    <!-- ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~HEADER~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ -->
    <!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">  -->
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html PUBLIC &quot;-//W3C//DTD XHTML 1.0 Strict//EN&quot; &quot;http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd&quot;&gt;</xsl:text>
    <html>
      <head>
        <title>INCIDENTS REPORT</title>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.2.1/css/bootstrap.min.css" />
      </head>
      <body class="bg-light">
        <div class="navbar navbar-expand flex-column flex-md-row bd-navbar shadow" style="background-color: #aab7b8">
            <xsl:for-each select="authors/author">
              <img class="rounded-circle mx-2 shadow" width="72" height="72">
                <xsl:attribute name="src">
                  <xsl:value-of select="@id"/>
                  <xsl:text>.png</xsl:text>
                </xsl:attribute>
                <xsl:attribute name="alt">
                  <xsl:value-of select="."/>
                  <xsl:text> </xsl:text>
                  <xsl:value-of select="@id"/>
                </xsl:attribute>
              </img>
            </xsl:for-each>
          
          <h2 class="mx-2">
            <b>Incidents Report</b>
          </h2>
        </div>
        <!-- ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~HEADER END~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ -->

        <xsl:apply-templates select="stats"/>
        <xsl:apply-templates select="incidents"/>

        <!-- ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~FOOTER~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ -->
      </body>
    </html>
    <!-- ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~FOOTER END~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ -->
  </xsl:template>

  <xsl:template match="incidents">
    <xsl:for-each select="incident">
      <xsl:sort select="priority" />
      <div class="container">
        <h3 class="d-flex justify-content-between align-items-center mb-1 ml-1 text-muted">
          #<xsl:value-of select="position()" />
        </h3>
      </div>
      <div class="container card shadow mb-4" style="background-color: #CCCCCC">
        <div class="row mt-2 mx-0">
          <div class="col-md-4 order-md-1 py-1 card">
            <div class="card-boody">
              Incident ID:  <xsl:value-of select="incidentID" />
            </div>
          </div>

          <div class="col-md-4 order-md-2 py-1 card">
            <div class="card-boody">
              User:  <xsl:value-of select="endUser" />
            </div>
          </div>

          <div class="col-md-4 order-md-3 py-1 card">
            <div class="card-boody">
              Date: <xsl:value-of select="dateOpened" />
            </div>
          </div>
        </div>

        <div class="row mt-2 mx-0">
          <div class="col-md-4 order-md-1 py-1 card">
            <div class="card-boody">
              State: <xsl:value-of select="state" />
            </div>
          </div>

          <div class="col-md-8 order-md-2 py-1 card">
            <div class="card-boody">
              Category: <xsl:value-of select="category/mainCategory" />/ <xsl:value-of select="category/subcategory" />
            </div>
          </div>
        </div>

        <div class="row mt-2 mx-0">
          <div class="col-md-4 order-md-1 py-1 card">
            <div class="card-boody">
              Priority: <xsl:value-of select="priority" />
            </div>
          </div>

          <div class="col-md-4 order-md-2 py-1 card">
            <div class="card-boody">
              Group: <xsl:value-of select="assignmentGroup" />
            </div>
          </div>

          <div class="col-md-4 order-md-3 py-1 card">
            <div class="card-boody">
              Phone: <xsl:value-of select="phoneNumber" />
            </div>
          </div>
        </div>
        <div class="row mt-2 mx-0">
          <div class="col-md-12 py-1 card">
            <div class="card-boody">
              Short: <xsl:value-of select="shortDescription" />
            </div>
          </div>
        </div>
        <div class="row mt-2 mx-0">
          <div class="col-md-12 py-1 card">
            <div class="card-boody">
              Description: <xsl:value-of select="description" />
            </div>
          </div>
        </div>
        <div class="row my-2 mx-0">
          <div class="col-md-12 py-1 card">
            <div class="card-boody">
              Notes: <xsl:value-of select="workNotes" />
            </div>
          </div>
        </div>
      </div>
    </xsl:for-each>

  </xsl:template>

  <!-- ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~STATISTICS~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ -->
  <xsl:template match="stats">
    <div class="container">
      <div class="row mt-2">
        <div class="col-md-6">
          <h3 class="d-flex justify-content-between align-items-center mb-1 ml-1 text-muted">STATISTICS</h3>
          <ul class="list-group mb-3">
            <li class="list-group-item d-flex justify-content-between">
              <div>
                <small>
                  Number of incidents: <xsl:value-of select="numberOfIncidents" />
                </small>
              </div>
            </li>
            <li class="list-group-item d-flex justify-content-between">
              <div>
                <small>
                  Most common category: <xsl:value-of select="mostCommonCategory" />
                </small>
              </div>
            </li>
            <li class="list-group-item d-flex justify-content-between">
              <div>
                <small>
                  Report generation date: <xsl:value-of select="reportDate" />
                </small>
              </div>
            </li>
          </ul>
        </div>
      </div>
    </div>
  </xsl:template>
  <!-- ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~STATISTICS END~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ -->

</xsl:stylesheet>
